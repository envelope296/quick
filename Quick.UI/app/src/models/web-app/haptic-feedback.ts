export type ImpactStyle = 'light' | 'medium' | 'heavy' | 'rigid' | 'soft';

export type NotificationType = 'error' | 'success' | 'warning';

export interface HapticFeedback {
  impactOccurred(impactStyle: ImpactStyle, disableVibrationFallback?: boolean): void;
  notificationOccurred(notificationType: NotificationType, disableVibrationFallback?: boolean): void;
  selectionChanged(disableVibrationFallback?: boolean): void;
}