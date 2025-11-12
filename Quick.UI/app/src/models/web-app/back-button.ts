export interface BackButton {
  readonly isVisible: boolean;
  show(): void;
  hide(): void;
  onClick(callback: VoidFunction): void;
  offClick(callback: VoidFunction): void;
}