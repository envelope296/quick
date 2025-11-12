import { useState } from 'react';


interface BooleanStateModifier {
    setTrue: VoidFunction,
    setFalse: VoidFunction,
    setToggle: VoidFunction,
}

export function useBoolean(initialState: boolean = false): [boolean, BooleanStateModifier] {
    const [state, setState] = useState(initialState);

    const handleTrue = () => setState(true);
    const handleFalse = () => setState(false);
    const handleToggle = () => setState(!state);

    return [
        state,
        {
            setTrue: handleTrue,
            setFalse: handleFalse,
            setToggle: handleToggle,
        },
    ];
};