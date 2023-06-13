import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthenticationInterceptor } from './shared/interceptors/authentication.interceptor';
import { NavbarComponent } from './ui-utils/navbar/navbar.component';
import { MangaProductComponent } from './manga-product/manga-product.component';
import { ChapterComponent } from './chapter/chapter.component';

@NgModule({
  declarations: [AppComponent, NavbarComponent, MangaProductComponent, ChapterComponent],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: AuthenticationInterceptor, multi: true }],
  bootstrap: [AppComponent],
})
export class AppModule {}
