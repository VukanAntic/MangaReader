import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { HomepageRoutingModule } from "./homepage-routing.module";
import { HomepageComponent } from "./homepage.component";
import { MangaImageComponent } from "./feature-genres/manga-image/manga-image.component";
import { MangaListComponent } from "./feature-genres/manga-list/manga-list.component";

@NgModule({
  declarations: [HomepageComponent, MangaImageComponent, MangaListComponent],
  imports: [CommonModule, HomepageRoutingModule],
})
export class HomepageModule {}
