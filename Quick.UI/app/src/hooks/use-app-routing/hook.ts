import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import * as AppContext from '@/services/app-context';
import type { Supplier } from '@/types/functions';

export function useAppRouting(previousPath: Supplier<string> | null = null) {
    const navigate = useNavigate();
    const isRoot = previousPath == null;

    const toPrevious = async () => {
        if (previousPath != null) {
            await navigate(previousPath());
        }
    }

    useEffect(() => {
        const webApp = AppContext.getWebApp();
        if (!isRoot) {
            webApp.BackButton.onClick(toPrevious);
            webApp.BackButton.show();

            return () => {
                webApp.BackButton.offClick(toPrevious);
                webApp.BackButton.hide();
            };
        }
    }, [isRoot]);

    return toPrevious;
}