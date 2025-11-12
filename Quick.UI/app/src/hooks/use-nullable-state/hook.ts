import { useState } from 'react';
import type { Consumer } from '@/types/functions';


interface NullableStateModifier<T> {
    set: Consumer<T>,
    clear: VoidFunction,
}

export function useNullableState<T>(initialState: T | null = null): [T | null, NullableStateModifier<T>] {
    const [state, setState] = useState(initialState);

    const handleSet = (state: T) => setState(state);
    const handleClear = () => setState(null);

    return [
        state,
        {
            set: handleSet,
            clear: handleClear
        },
    ];
};