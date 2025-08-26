import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../../../material.module';
import { WordService } from '../../../../core/services/word.service';
import { MatDialog } from '@angular/material/dialog';
import { AddWordDialogComponent } from '../../components/add-word-dialog/add-word-dialog.component';
import { CategoryService } from '../../../../core/services/category.service';

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
  private categoryService = inject(CategoryService);

  words: any[] = [];
  categories: any[] = [];
  displayedColumns: string[] = ['originalText', 'translation', 'partOfSpeech'];

  ngOnInit(): void {
    this.loadInitialData();
  }

  loadInitialData(): void {
    this.wordService.getWords().subscribe((data) => (this.words = data));
    this.categoryService.getCategories().subscribe((data) => {
      console.log('Categories loaded in parent:', data);
      this.categories = data;
    });
  }

  loadWords(): void {
    this.wordService.getWords().subscribe((data) => {
      this.words = data;
    });
  }

  openAddWordDialog(): void {
    const dialogRef = this.dialog.open(AddWordDialogComponent, {
      width: '400px',
      data: { categories: this.categories },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.wordService.createWord(result).subscribe(() => {
          console.log('Word created successfully!');
          this.loadInitialData();
        });
      }
    });
  }
}
