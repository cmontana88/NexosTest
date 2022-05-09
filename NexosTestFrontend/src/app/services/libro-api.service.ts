import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';
import { LibrosFiltrado } from '../models/LibrosFiltrado';

@Injectable({
  providedIn: 'root'
})
export class LibroApiService {

  serverUrl = 'https://localhost:44353/';

  constructor(private http: HttpClient) { }

  obtenerLibrosFiltrados(fieldFilter: string, criterioFilter: string, page: number, itemxpage: number) {
    return this.http.get<LibrosFiltrado>(this.serverUrl + 'api/Libro/' + page + '/' + itemxpage + (fieldFilter === '' ? '/ninguno' : '/') + fieldFilter + (criterioFilter === '' ? '/ninguno' : '/') + criterioFilter).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      console.error('An error occurred:', error.error);
    } else {
      console.error(
        `Backend returned code ${error.status}, body was: `, error.error);
    }
    return throwError(() => new Error('Something bad happened; please try again later.'));
  }

}
