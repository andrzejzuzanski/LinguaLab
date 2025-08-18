import { Component, inject, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MaterialModule } from '../../../../material.module';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-word-dialog',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MaterialModule],
  templateUrl: './add-word-dialog.component.html',
  styleUrls: ['./add-word-dialog.component.scss'],
})
export class AddWordDialogComponent implements OnInit {
  wordForm!: FormGroup;

  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialogRef<AddWordDialogComponent>);

  ngOnInit(): void {
    this.wordForm = this.fb.group({
      originalText: ['', Validators.required],
      translation: ['', Validators.required],
      categoryId: ['', Validators.required],
      partOfSpeech: [''],
      exampleSentence: [''],
    });
  }

  onSave(): void {
    if (this.wordForm.valid) {
      this.dialogRef.close(this.wordForm.value);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
