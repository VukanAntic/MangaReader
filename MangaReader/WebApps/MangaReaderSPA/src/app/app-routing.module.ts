import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MangaProductComponent } from './manga-product/manga-product.component';

const routes: Routes = [
  { path: 'identity', loadChildren: () => import('./identity/identity.module').then(m => m.IdentityModule) },
  { path: 'manga/:mangaId', component : MangaProductComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
