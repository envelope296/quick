import type { StorageBase } from "./storage-base";

export interface DeviceStorage extends StorageBase {
    clear(): void;
}