import { useState } from "react";
import Select, { type ActionMeta, type OnChangeValue } from 'react-select';
import { Button } from '@maxhub/max-ui';
import { type NotificationType } from "@/models/web-app/haptic-feedback";
import * as AppContext from '@/services/app-context';

export default function App() {
  const [count, setCount] = useState<number>(0);
  const [impactStyle, setImpactStyle] = useState<NotificationType>('error');

  interface ImpactStyleOption {
    value: NotificationType,
    label: string
  }

  const hapticOptions : ImpactStyleOption[] = [
    { value: 'error', label: 'error' },
    { value: 'success', label: 'success' },
    { value: 'warning', label: 'warning' },
  ]

  function hapticTest() {
    const webApp = AppContext.getWebApp();
    webApp.HapticFeedback.notificationOccurred(impactStyle, false);
  }

  function onImpactStyleChange(newValue: OnChangeValue<ImpactStyleOption, false>, actionMeta: ActionMeta<ImpactStyleOption>) {
    if (newValue) {
      setImpactStyle(newValue.value);
    } else {
      setImpactStyle('error');
    }
  }

  function onBackButtonClicked() {
    setCount(count + 1);
  }

  return (
    <div>
      <Button 
        mode="primary" 
        appearance="themed" 
        size="medium" 
        onClick={hapticTest}
      >
        Haptic Feedback
      </Button>
      <Select 
        options={hapticOptions}
        onChange={onImpactStyleChange}
      />

      <Button
        onClick={() => {
          const webApp = AppContext.getWebApp();
          webApp.BackButton.show();
          webApp.BackButton.onClick(onBackButtonClicked);
        }}
      >
        Включить кнопку "Назад"
      </Button>
      <Button
        onClick={() => {
          const webApp = AppContext.getWebApp();
          
          webApp.BackButton.offClick(onBackButtonClicked);
          webApp.BackButton.hide();
        }}
      >
        Выключить кнопку "Назад"
      </Button>
      <p>Счетчик: {count}</p>
    </div>
  );
}