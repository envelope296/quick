export interface StorageBase {
    setItem(key: string, value: string): void;
    getItem(key: string): string;
    removeItem(key: string): string;
}