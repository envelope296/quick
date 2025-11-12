export type Consumer<T> = (value: T) => void;

export type Supplier<T> = () => T;

export type Predicate<T> = (value: T) => boolean;

export type AsyncVoidFunction = () => Promise<void>;

export type AsyncConsumer<T> = (value: T) => Promise<void>;

export type CallbackFunction<T> = (callback: Consumer<T>) => void;