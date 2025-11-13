export interface Option {
    value: string,
    label: string
}

export function createOption(label: string): Option {
  return {
    label,
    value: label,
  };
}