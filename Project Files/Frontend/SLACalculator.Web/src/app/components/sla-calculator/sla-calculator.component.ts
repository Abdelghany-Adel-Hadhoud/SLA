import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SlaService } from '../../services/sla.service';
import { Priority, PriorityLabels } from '../../models/priority';
import { SlaRequest } from '../../models/sla-request';
import { SlaResponse } from '../../models/sla-response';

@Component({
  selector: 'app-sla-calculator',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './sla-calculator.component.html',
  styleUrls: ['./sla-calculator.component.css'],
})
export class SlaCalculatorComponent {
  selectedPriority: Priority = Priority.High;
  captureDate: string = '';
  captureTime: string = '';
  selectedFiles: File[] = [];

  slaResult: SlaResponse | null = null;
  loading: boolean = false;
  error: string | null = null;
  uploadedFileNames: string[] = [];

  priorities = [
    { value: Priority.High, label: PriorityLabels[Priority.High] },
    { value: Priority.Medium, label: PriorityLabels[Priority.Medium] },
    { value: Priority.Low, label: PriorityLabels[Priority.Low] },
  ];

  constructor(private slaService: SlaService) {
    const now = new Date();
    this.captureDate = now.toISOString().split('T')[0];
    this.captureTime = now.toTimeString().slice(0, 5);
  }

  onFileSelected(event: any): void {
    const files: FileList = event.target.files;
    this.selectedFiles = Array.from(files);
  }

  async calculateSla(): Promise<void> {
    this.error = null;
    this.loading = true;
    this.slaResult = null;

    try {
      if (this.selectedFiles.length > 0) {
        const uploadResult = await this.slaService
          .uploadFiles(this.selectedFiles)
          .toPromise();
        this.uploadedFileNames = uploadResult.files;
      }
      const captureDateTime = `${this.captureDate}T${this.captureTime}:00`;

      const request: SlaRequest = {
        priority: +this.selectedPriority,
        captureDateTime: captureDateTime,
        fileNames: this.uploadedFileNames,
      };

      this.slaResult =
        (await this.slaService.calculateSla(request).toPromise()) || null;
    } catch (err: any) {
      this.error =
        err.error?.error || 'An error occurred while calculating SLA';
      console.error('Error:', err);
    } finally {
      this.loading = false;
    }
  }

  reset(): void {
    const now = new Date();
    this.captureDate = now.toISOString().split('T')[0];
    this.captureTime = now.toTimeString().slice(0, 5);
    this.selectedPriority = Priority.High;
    this.selectedFiles = [];
    this.uploadedFileNames = [];
    this.slaResult = null;
    this.error = null;
  }

  formatDateTime(dateTimeStr: string): string {
    const date = new Date(dateTimeStr);
    return date.toLocaleString('en-US', {
      year: 'numeric',
      month: 'long',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit',
      second: '2-digit',
    });
  }

  getPriorityLabel(priority: Priority): string {
    return PriorityLabels[priority];
  }
}
