import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MangaProductComponent } from './manga-product/manga-product.component';
import { ChapterComponent } from './chapter/chapter.component';

const routes: Routes = [
  { path: 'identity', loadChildren: () => import('./identity/identity.module').then((m) => m.IdentityModule) },
  { path: 'manga/:mangaId', component: MangaProductComponent },
  { path: 'chapter/:chapterId', component: ChapterComponent },
  {
    path: 'recommended',
    loadChildren: () => import('./recommended/recommended.module').then((m) => m.RecommendedModule),
  },
  { path: 'homepage', loadChildren: () => import('./homepage/homepage.module').then(m => m.HomepageModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
