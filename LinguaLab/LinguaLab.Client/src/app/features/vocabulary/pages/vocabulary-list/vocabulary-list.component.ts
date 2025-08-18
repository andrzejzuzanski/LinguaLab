import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../../../material.module';
import { WordService } from '../../../../core/services/word.service';
import { MatDialog } from '@angular/material/dialog';
import { AddWordDialogComponent } from '../../components/add-word-dialog/add-word-dialog.component';

@Component({
  selector: 'app-vocabulary-list',
  standalone: true,
  imports: [CommonModule, MaterialModule],
  templateUrl: './vocabulary-list.component.html',
  styleUrl: './vocabulary-list.component.scss',
})
export class VocabularyListComponent implements OnInit {
  private wordService = inject(WordService);
  private dialog = inject(MatDialog);

  words: any[] = [];
  displayedColumns: string[] = ['originalText', 'translation', 'partOfSpeech'];

  ngOnInit(): void {
    this.loadWords();
  }

  loadWords(): void {
    this.wordService.getWords().subscribe((data) => {
      this.words = data;
    });
  }

  openAddWordDialog(): void {
    const dialogRef = this.dialog.open(AddWordDialogComponent, {
      width: '400px',
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed', result);
      if (result) {
        this.wordService.createWord(result).subscribe(() => {
          console.log('Word created successfully!');
          this.loadWords();
        });
      }
    });
  }
}
