import { ActivatedRoute, Route } from '@angular/router';
import { MangaProductService } from './domain/infrastructure/manga-product.service';
import { Component, OnInit } from '@angular/core';
import { Manga } from './domain/models/manga.model';
import { Chapter } from './domain/models/chapter.model';

@Component({
  selector: 'app-manga-product',
  templateUrl: './manga-product.component.html',
  styleUrls: ['./manga-product.component.css']
})
export class MangaProductComponent implements OnInit {

  public mangaId! : string;
  public mangaData! : Manga;
  public mangaChapters! : [Chapter];

  constructor(private mangaProductService : MangaProductService, private route : ActivatedRoute) {
   }

  ngOnInit(): void {
    // TODO: better way of showing a 404
    this.route.paramMap.subscribe((params) => {
      this.mangaId = String(params.get("mangaId"));
      this.mangaProductService.getMangaData(this.mangaId).subscribe(
        (res) => {
          this.mangaData = res;
        }, 
      );
      this.mangaProductService.getMangaChapters(this.mangaId).subscribe((res) => {
        this.mangaChapters = res;
        console.log(this.mangaChapters);
      });  
    });
    
   
  }

}
