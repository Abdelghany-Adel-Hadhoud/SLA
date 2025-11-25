import { Priority } from './priority';

export interface SlaResponse {
  priority: Priority;
  captureDateTime: string;
  slaExpirationDateTime: string;
  slaTimeInHours: number;
}