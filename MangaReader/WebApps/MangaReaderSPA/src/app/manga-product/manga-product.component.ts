import { addToWishlistItem } from './domain/models/addToWishlistItem.model';
import { ActivatedRoute, Route, Router } from '@angular/router';
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
  public mangaChapters! : Chapter[];

  constructor(private mangaProductService : MangaProductService, private route : ActivatedRoute, private router: Router) {
   }

  ngOnInit(): void {
    // TODO: better way of showing a 404
    this.route.paramMap.subscribe((params) => {
      this.mangaId = String(params.get("mangaId"));
      this.mangaProductService.getMangaData(this.mangaId).subscribe( {
        next: (res) => this.mangaData = res,
        error: (e) => this.router.navigate(['404']),
      });
      this.mangaProductService.getMangaChapters(this.mangaId).subscribe( {
        next: (res) => this.mangaChapters = res,
        error: (e) => this.router.navigate(['404']),
    });   
    });
  }

  addToWishlist() : void {
    var item = new addToWishlistItem();
    item.mangaId = this.mangaId;
    console.log(item);
    this.mangaProductService.getWishlistItem().subscribe((res) => {
      console.log(res);
    });
    this.mangaProductService.addToWishlist(item).subscribe((res) => {
      window.alert("Item added to wishlist!");
      console.log(res);
    });
  }

}
