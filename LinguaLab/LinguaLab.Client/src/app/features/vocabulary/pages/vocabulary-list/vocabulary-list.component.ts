import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../../../material.module';
import { WordService } from '../../../../core/services/word.service';

@Component({
  selector: 'app-vocabulary-list',
  standalone: true,
  imports: [CommonModule, MaterialModule],
  templateUrl: './vocabulary-list.component.html',
  styleUrl: './vocabulary-list.component.scss',
})
export class VocabularyListComponent implements OnInit {
  private wordService = inject(WordService);

  words: any[] = [];
  displayedColumns: string[] = ['originalText', 'translation', 'partOfSpeech'];

  ngOnInit(): void {
    this.wordService.getWords().subscribe((data) => {
      this.words = data;
    });
  }
}
