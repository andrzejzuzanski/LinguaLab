import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../../../material.module';
import { LearningService } from '../../../../core/services/learning.service';

@Component({
  selector: 'app-learning-session',
  standalone: true,
  imports: [CommonModule, MaterialModule],
  templateUrl: './learning-session.component.html',
  styleUrl: './learning-session.component.scss',
})
export class LearningSessionComponent implements OnInit {
  private learningService = inject(LearningService);

  words: any[] = [];
  currentWordIndex = 0;
  isFlipped = false;

  readonly answerQualities = [
    { quality: 1, label: 'Wrong', icon: 'sentiment_very_dissatisfied' },
    { quality: 3, label: 'Hard', icon: 'sentiment_dissatisfied' },
    { quality: 5, label: 'Easy', icon: 'sentiment_very_satisfied' },
  ];

  ngOnInit(): void {
    this.startNewSession();
  }

  startNewSession(): void {
    this.learningService.getSessionWords().subscribe((data) => {
      this.words = data;
      this.currentWordIndex = 0;
      this.isFlipped = false;
    });
  }

  get currentWord(): any {
    return this.words[this.currentWordIndex];
  }

  flipCard(): void {
    this.isFlipped = !this.isFlipped;
  }

  submitAnswer(quality: number): void {
    if (!this.currentWord) return;

    this.learningService
      .submitAnswer(this.currentWord.id, quality)
      .subscribe(() => {
        this.goToNextWord();
      });
  }
  goToNextWord(): void {
    this.currentWordIndex++;
    this.isFlipped = false;
  }
}
