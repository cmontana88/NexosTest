import { TestBed } from '@angular/core/testing';

import { LibroApiService } from './libro-api.service';

describe('LibroApiService', () => {
  let service: LibroApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LibroApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
