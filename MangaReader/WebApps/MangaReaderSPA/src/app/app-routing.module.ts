import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { MangaProductComponent } from "./manga-product/manga-product.component";
import { ChapterComponent } from "./chapter/chapter.component";
import { SearchComponent } from "./search/search.component";
import { ErrorComponent } from "./error/error.component";


const routes: Routes = [
  { path: 'identity', loadChildren: () => import('./identity/identity.module').then((m) => m.IdentityModule) },
  { path: 'manga/:mangaId', component: MangaProductComponent },
  { path: 'chapter/:chapterId', component: ChapterComponent },
  {
    path: 'recommended',
    loadChildren: () => import('./recommended/recommended.module').then((m) => m.RecommendedModule),
  },
  {path: "search/:searchPrefix", component: SearchComponent},
  {path: "404", component: ErrorComponent},
  { path: 'homepage', loadChildren: () => import('./homepage/homepage.module').then(m => m.HomepageModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    scrollPositionRestoration: 'enabled'
  })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
