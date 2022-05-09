import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TablaLibrosComponent } from './components/tabla-libros/tabla-libros.component';

const routes: Routes = [
  { path: '', component: TablaLibrosComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
