import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TablaLibrosComponent } from './tabla-libros.component';

describe('TablaLibrosComponent', () => {
  let component: TablaLibrosComponent;
  let fixture: ComponentFixture<TablaLibrosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TablaLibrosComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TablaLibrosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
