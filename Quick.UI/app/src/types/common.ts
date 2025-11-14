export interface Option {
    value: string,
    label: string
}

export interface EntityOption {
  label: string,
  id: string,
  value: string
}

export function createOption(label: string): Option {
  return {
    label,
    value: label,
  };
}

export function createEntityOption(label: string, id: string): EntityOption {
  return {
    label,
    id,
    value: label
  }
}