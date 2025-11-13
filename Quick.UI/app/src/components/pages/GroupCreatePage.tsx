import React from "react";
import { useState } from "react";
import CreatableSelect from "react-select/creatable";
import styles from "./GroupCreatePage.module.css";
import { useBoolean, useNullableState } from "@/hooks";
import {type Option, createOption} from '@/types/common';
import { useAppRouting } from "@/hooks/use-app-routing";
import * as groupServise from "@/services/group";

export function GroupCreatePage() {
  const toPrevios = useAppRouting(() => '/');

  const components = {
    DropdownIndicator: null,
  };

  const [groupName, {set: setGroupName, clear: clearGroupName}] = useNullableState<string>();
  const [subgroupNames, setSubgroupNames] = useState<readonly string[]>([]);
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

  function onUniversityInputChanged(e: React.ChangeEvent<HTMLInputElement>) {
    const newValue = e.target.value;
    if (newValue === null || newValue === "") {
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
    await groupServise.createGroup(request);
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
              className={styles.inputField}
              placeholder="Название"
              onChange={onGroupNameInputChanged}
            />

            <input
              type="text"
              className={styles.inputField}
              placeholder="Университет"
              onChange={onUniversityInputChanged}
            />

            <CreatableSelect
              components={components}
              isMulti
              isClearable
              placeholder="Подгруппы"
              closeMenuOnSelect={subgroupNameOptions.length == 0}
              openMenuOnClick={subgroupNameOptions.length != 0}
              openMenuOnFocus={subgroupNameOptions.length != 0}
              options={subgroupNameOptions}
              noOptionsMessage={() => "Введите название подгруппы"}
              formatCreateLabel={(value) => `Добавить ${value}`}
              onChange={(newValue) => setSubgroupNames(newValue.map(opt => opt.value))}
              classNames={{
                control: () => styles.inputSelect,
                multiValueLabel: () => styles.inputSelectLabel,
                multiValueRemove: () => styles.inputSelectRemove,
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
              onClick={() => {}}
            >
              Создать
            </button>
          </div>
        </main>
      </div>
      
      {subgroupNames.map(n => <p>{n}</p>)}

    </section>
  );
}
