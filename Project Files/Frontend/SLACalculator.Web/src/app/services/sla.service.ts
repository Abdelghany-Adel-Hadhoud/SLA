import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SlaRequest } from '../models/sla-request';
import { SlaResponse } from '../models/sla-response';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SlaService {
  private apiUrl = `${environment.apiUrl}/api/sla`;

  constructor(private http: HttpClient) { }

  calculateSla(request: SlaRequest): Observable<SlaResponse> {
    return this.http.post<SlaResponse>(`${this.apiUrl}/calculateSla`, request);
  }

  uploadFiles(files: File[]): Observable<any> {
    const formData = new FormData();
    files.forEach(file => {
      formData.append('files', file, file.name);
    });
    return this.http.post(`${this.apiUrl}/upload`, formData);
  }
}