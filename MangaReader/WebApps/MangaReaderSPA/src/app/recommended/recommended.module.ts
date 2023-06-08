import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'

import { RecommendedRoutingModule } from './recommended-routing.module'
import { RecommendedComponent } from './recommended.component'
import { MangaImageComponent } from './feature-recommendation/manga-image/manga-image.component'

@NgModule({
  declarations: [RecommendedComponent, MangaImageComponent],
  imports: [CommonModule, RecommendedRoutingModule],
})
export class RecommendedModule {}
