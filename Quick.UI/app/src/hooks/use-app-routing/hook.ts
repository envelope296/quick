import { useEffect, useCallback } from 'react';
import { useNavigate } from 'react-router-dom';
import * as AppContext from '@/services/app-context';

export function useAppRouting(previousPath: string | null = null) {
    const navigate = useNavigate();
    const isRoot = previousPath == null;

    const toPrevious = useCallback(() => {
        if (previousPath != null) {
            navigate(previousPath);
        }
    }, [previousPath, navigate]);

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
    }, [isRoot, toPrevious]);
}