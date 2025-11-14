import styles from "./GroupJoinPage.module.css";
import { useBoolean, useNullableState } from "@/hooks";
import {type EntityOption, type Option, createEntityOption, createOption} from '@/types/common';
import { useAppRouting } from "@/hooks/use-app-routing";
import * as groupServise from "@/services/group";
import { useNavigate } from "react-router-dom";
import * as dadataService from "@/services/dadata";
import AsyncSelect from "react-select/async";
import AsyncCreatableSelect from "react-select/async-creatable";

export function GroupJoinPage() {
  const navigate = useNavigate();
  const toPrevios = useAppRouting(() => '/');

  const [university, {set: setUniversity, clear: clearUniversity}] = useNullableState<string>();
  const [group, {set: setGroup, clear: clearGroup}] = useNullableState<EntityOption>();
  
  const [isGroupSelectDisabled, { setTrue: disableGroupSelect, setFalse: enableGroupSelect }] = useBoolean(true);
  const [isJoinButtonDisabled, { setTrue: disableJoinButton, setFalse: enableJoinButton }] = useBoolean(true);

  function onUniversityChanged(newValue: string | null | undefined) {
    clearGroup();
    
    if (newValue === undefined || newValue === null || newValue === "") {
      clearUniversity();
      disableGroupSelect();
    }
    else {
      setUniversity(newValue);
      enableGroupSelect();
    }
  }

  function onGroupChanged(newValue: EntityOption | null | undefined) {
    if (newValue === undefined || newValue === null) {
      clearGroup();
      disableJoinButton();
    }
    else {
      setGroup(newValue);
      enableJoinButton();
    }
  }

  async function onJoinPressed() {
    if (university === null || group === null) {
      return;
    }

    const response = await groupServise.join(group.id);
    await navigate(`/groups/${group.id}`);
  }

  async function loadUniversities(query: string): Promise<Option[]> {
    const response = await dadataService.suggestUniversities(query);
    return response.suggestions.map(s => createOption(s.value));
  }

  async function loadGroups(query: string): Promise<EntityOption[]> {
    if (university === null) {
      return [];
    }

    const request = {
      page: 1,
      size: 100,
      query,
      university
    }
    const groupsPage = await groupServise.search(request);
    return groupsPage.items.map(g => createEntityOption(g.name, g.id));
  }

  return (
    <section className={styles.createGroupModal}>
      <div className={styles.modalCard}>
        <header className={styles.modalHeader}>
          <h1 className={styles.modalTitle}>Поиск группы</h1>
        </header>
        <main className={styles.modalBody}>
          <div className={styles.formGroup}>
            <AsyncCreatableSelect
              createOptionPosition="first"
              isClearable
              cacheOptions
              loadOptions={loadUniversities}
              classNames={{
                control: () => "input-select"
              }}
              formatCreateLabel={(value) => value}
              loadingMessage={() => "Поиск..."}
              placeholder="Университет"
              onChange={(newValue, _) => onUniversityChanged(newValue?.value)}
              noOptionsMessage={() => "Введите название университета"}
            />

            <AsyncSelect
              isClearable
              loadOptions={loadGroups}
              classNames={{
                control: (state) => state.isDisabled ? "input-select input-select-disabled" : "input-select"
              }}
              isDisabled={isGroupSelectDisabled}
              loadingMessage={() => "Поиск..."}
              placeholder="Группа"
              onChange={(newValue, _) => onGroupChanged(newValue)}
              noOptionsMessage={() => "Группы не найдены"}
              value={group}
            />
          </div>

          <div className={styles.modalActions}>
            <button 
              className={`${styles.btn} ${styles.btnCancel}`}
              onClick={toPrevios}  
            >
              Отмена
            </button>
            <button
              disabled={isJoinButtonDisabled}
              className={`${styles.btn} ${styles.btnJoin}`}
              onClick={onJoinPressed}
            >
              Присоединиться
            </button>
          </div>
        </main>
      </div>
    </section>
  );
}
