import React from "react";
import { useState } from "react";
import CreatableSelect from "react-select/creatable";
import styles from "./GroupCreatePage.module.css";
import { useNullableState } from "@/hooks";
import {type Option, createOption} from '@/types/common';

export function GroupCreatePage() {
  const components = {
    DropdownIndicator: null,
  };

  const [groupName, {set: setGroupName}] = useNullableState<string>();

  const [subgroupNames, setSubgroupNames] = useState<string[]>([]);
  const [subgroupNameOptions, setSubgroupNameOptions] = useState<Option[]>([]);

  function onGroupNameInputChanged(e: React.ChangeEvent<HTMLInputElement>) {
    const newName = e.target.value;
    setGroupName(newName);

    if (newName === null || newName === "") {
      setSubgroupNameOptions([]);
      return;
    }

    const optionsCount = 2;
    const nameOptions: Option[] = [];

    for (var i = 1; i <= optionsCount; i++) {
      nameOptions.push(createOption(`${newName} (${i})`));
    }
    setSubgroupNameOptions(nameOptions);
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
              classNames={{
                control: () => styles.inputSelect,
                multiValueLabel: () => styles.inputSelectLabel,
                multiValueRemove: () => styles.inputSelectRemove,
              }}
            ></CreatableSelect>
          </div>

          <div className={styles.modalActions}>
            <button className={`${styles.btn} ${styles.btnCancel}`}>
              Отмена
            </button>
            <button
              className={`${styles.btn} ${styles.btnCreate}`}
              onClick={() => {}}
            >
              Создать
            </button>
          </div>
        </main>
      </div>
    </section>
  );
}
