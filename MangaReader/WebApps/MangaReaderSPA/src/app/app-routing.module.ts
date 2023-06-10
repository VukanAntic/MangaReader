import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MangaProductComponent } from './manga-product/manga-product.component';
import { ChapterComponent } from './chapter/chapter.component';

const routes: Routes = [
  { path: 'identity', loadChildren: () => import('./identity/identity.module').then(m => m.IdentityModule) },
  { path: 'manga/:mangaId', component : MangaProductComponent},
  { path: 'chapter/:chapterId', component : ChapterComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
