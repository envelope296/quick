import { useState } from "react";
import Select, { type ActionMeta, type OnChangeValue } from 'react-select';
import { Button } from '@maxhub/max-ui';
import { type ImpactStyle } from "@/models/web-app/haptic-feedback";
import * as AppContext from '@/services/app-context';

export default function App() {
  const [impactStyle, setImpactStyle] = useState<ImpactStyle>('light');

  interface ImpactStyleOption {
    value: ImpactStyle,
    label: string
  }

  const hapticOptions : ImpactStyleOption[] = [
    { value: 'light', label: 'light' },
    { value: 'medium', label: 'medium' },
    { value: 'heavy', label: 'heavy' },
    { value: 'rigid', label: 'rigid' },
    { value: 'soft', label: 'soft' }
  ]

  function hapticTest() {
    const webApp = AppContext.getWebApp();
    webApp.HapticFeedback.impactOccurred(impactStyle, false);
  }

  function onImpactStyleChange(newValue: OnChangeValue<ImpactStyleOption, false>, actionMeta: ActionMeta<ImpactStyleOption>) {
    if (newValue) {
      setImpactStyle(newValue.value);
    } else {
      setImpactStyle('light');
    }
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
    </div>
  );
}