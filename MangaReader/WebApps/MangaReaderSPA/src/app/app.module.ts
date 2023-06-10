import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { HttpClientModule } from "@angular/common/http";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { MangaProductComponent } from './manga-product/manga-product.component';
import { ChapterComponent } from './chapter/chapter.component';

@NgModule({
  declarations: [AppComponent, MangaProductComponent, ChapterComponent],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
