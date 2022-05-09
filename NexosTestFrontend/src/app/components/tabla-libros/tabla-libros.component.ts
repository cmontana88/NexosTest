import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Libro } from 'src/app/models/Libro';
import { LibrosFiltrado } from 'src/app/models/LibrosFiltrado';
import { LibroApiService } from 'src/app/services/libro-api.service';

@Component({
  selector: 'app-tabla-libros',
  templateUrl: './tabla-libros.component.html',
  styleUrls: ['./tabla-libros.component.css']
})
export class TablaLibrosComponent implements OnInit {

  libros:Array<Libro> = [];
  error: string = '';
  totalItemxPage: number = 3;
  totalPage: number = 0;
  page: number = 1;

  fieldFilter: string = '';
  criterioFilter: string = '';

  paginas: Array<number> = [];

  constructor(private libroService: LibroApiService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.filtrar();
  }

  filtrar(){
    this.libroService.obtenerLibrosFiltrados(this.fieldFilter, this.criterioFilter, this.page, this.totalItemxPage).subscribe(
      res => {
        this.libros = res.Libros as unknown as Array<Libro>;
        this.totalPage = res.TotalPages;
        console.log(this.libros);
        this.llenarPages();
      }
    );
  }

  showSuccess(message: string) {
    this.toastr.success(message, '');
  }

  showError(message: string) {
    this.toastr.error(message, '');
  }

  llenarPages() {
    this.paginas = [];
    for (let index = 1; index <= this.totalPage; index++) {
      this.paginas.push(index);
    }
  }

  setPage(pagina: number){
    this.page = pagina;
    this.filtrar();
  }

  nextPage(){
    if((this.page + 1) <= this.totalPage)
      this.setPage(this.page + 1);
  }

  previusPage(){
    if((this.page - 1) >= 1)
      this.setPage(this.page - 1);
  }

}
