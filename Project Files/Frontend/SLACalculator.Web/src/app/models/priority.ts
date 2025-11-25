
export enum Priority {
  High = 1,
  Medium = 2,
  Low = 3
}

export const PriorityLabels = {
  [Priority.High]: 'High (4 hours)',
  [Priority.Medium]: 'Medium (10 hours)',
  [Priority.Low]: 'Low (24 hours)'
};