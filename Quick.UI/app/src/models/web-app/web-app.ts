import type { BackButton } from "./back-button";
import type { DeviceStorage } from "./device-storage";
import type { HapticFeedback } from "./haptic-feedback";
import type { SecureStorage } from "./secure-storage";

export interface WebApp {
    initData: string,
    ready: VoidFunction,
    close: VoidFunction,
    BackButton: BackButton,
    HapticFeedback: HapticFeedback,
    enableClosingConfirmation: VoidFunction,
    disableClosingConfirmation: VoidFunction,
    DeviceStorage: DeviceStorage,
    SecureStorage: SecureStorage
}