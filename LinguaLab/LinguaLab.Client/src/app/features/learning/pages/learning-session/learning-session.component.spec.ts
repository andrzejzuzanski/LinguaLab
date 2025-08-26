import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LearningSessionComponent } from './learning-session.component';

describe('LearningSessionComponent', () => {
  let component: LearningSessionComponent;
  let fixture: ComponentFixture<LearningSessionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LearningSessionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LearningSessionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
