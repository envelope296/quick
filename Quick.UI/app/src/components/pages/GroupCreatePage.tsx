import React from "react";
import { useState } from "react";
import CreatableSelect from "react-select/creatable";
import styles from "./GroupCreatePage.module.css";
import { useBoolean, useNullableState } from "@/hooks";
import {type Option, createOption} from '@/types/common';
import { useAppRouting } from "@/hooks/use-app-routing";
import * as groupServise from "@/services/group";
import { useNavigate } from "react-router-dom";
import AsyncCreatableSelect from "react-select/async-creatable";
import * as dadataService from "@/services/dadata";

export function GroupCreatePage() {
  const navigate = useNavigate();
  const toPrevios = useAppRouting(() => '/');

  const [groupName, {set: setGroupName, clear: clearGroupName}] = useNullableState<string>();
  const [subgroupNames, setSubgroupNames] = useState<string[]>([]);
  const [university, {set: setUniversity, clear: clearUniversity}] = useNullableState<string>();
  
  const [subgroupNameOptions, setSubgroupNameOptions] = useState<Option[]>([]);
  const [isCreateDisabled, { setTrue: setCreateDisabled, setFalse: unsetCreateDisabled }] = useBoolean(true);

  function onGroupNameInputChanged(e: React.ChangeEvent<HTMLInputElement>) {
    const newValue = e.target.value;

    if (newValue === null || newValue === "") {
      clearGroupName();
      setSubgroupNameOptions([]);
      setCreateDisabled();
    }
    else {
      setGroupName(newValue);

      const optionsCount = 2;
      const nameOptions: Option[] = [];

      for (var i = 1; i <= optionsCount; i++) {
        nameOptions.push(createOption(`${newValue} (${i})`));
      }
      setSubgroupNameOptions(nameOptions);
      
      unsetCreateDisabled();
    }
  }

  function onUniversityInputChanged(newValue: string | null | undefined) {
    
    if (newValue ===undefined || newValue === null || newValue === "") {
      clearUniversity();
    }
    else {
      setUniversity(newValue);
    }
  }

  async function onCreatePressed() {
    if (groupName === null || groupName === "") {
      return;
    }

    const request = {
      name: groupName,
      university: university,
      subgroups: subgroupNames
    }
    const groupId = await groupServise.createGroup(request);

    await navigate(`/groups/${groupId}`);
  }

  async function loadUniversities(query: string): Promise<Option[]> {
    const response = await dadataService.suggestUniversities(query);
    return response.suggestions.map(s => createOption(s.value));
  }

  return (
    <section className={styles.createGroupModal}>
      <div className={styles.modalCard}>
        <header className={styles.modalHeader}>
          <h1 className={styles.modalTitle}>Создание группы</h1>
        </header>
        <main className={styles.modalBody}>
          <div className={styles.formGroup}>
            <input
              type="text"
              className="input-field"
              placeholder="Название"
              onChange={onGroupNameInputChanged}
            />

            <AsyncCreatableSelect
              createOptionPosition="first"
              isClearable
              cacheOptions
              loadOptions={loadUniversities}
              classNames={{
                control: () => "input-select"
              }}
              loadingMessage={() => "Поиск..."}
              placeholder="Университет"
              onChange={(newValue, _) => onUniversityInputChanged(newValue?.value)}
              formatCreateLabel={(value) => value}
              noOptionsMessage={() => "Введите название университета"}
            />

            <CreatableSelect
              createOptionPosition="first"
              isMulti
              isClearable
              placeholder="Подгруппы"
              closeMenuOnSelect={subgroupNameOptions.length == 0}
              openMenuOnClick={subgroupNameOptions.length != 0}
              openMenuOnFocus={subgroupNameOptions.length != 0}
              options={subgroupNameOptions}
              noOptionsMessage={() => "Введите название подгруппы"}
              formatCreateLabel={(value) => value}
              onChange={(newValue) => setSubgroupNames(newValue.map(opt => opt.value))}
              classNames={{
                control: () => "input-select",
                multiValueLabel: () => "input-select-label",
                multiValueRemove: () => "input-select-remove",
              }}
            ></CreatableSelect>
          </div>

          <div className={styles.modalActions}>
            <button 
              className={`${styles.btn} ${styles.btnCancel}`}
              onClick={toPrevios}  
            >
              Отмена
            </button>
            <button
              disabled={isCreateDisabled}
              className={`${styles.btn} ${styles.btnCreate}`}
              onClick={onCreatePressed}
            >
              Создать
            </button>
          </div>
        </main>
      </div>
    </section>
  );
}
