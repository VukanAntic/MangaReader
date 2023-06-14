import { addToUserInfoItem } from './domain/models/addToUserInfoItem';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { MangaProductService } from './domain/infrastructure/manga-product.service';
import { Component, OnInit } from '@angular/core';
import { Manga } from './domain/models/manga.model';
import { Chapter } from './domain/models/chapter.model';
import { Genre } from './domain/models/genre.model';

@Component({
  selector: 'app-manga-product',
  templateUrl: './manga-product.component.html',
  styleUrls: ['./manga-product.component.css']
})
export class MangaProductComponent implements OnInit {

  public mangaId! : string;
  public mangaData! : Manga;
  public mangaChapters! : Chapter[];
  public rating! : string;
  public allGenres! : [Genre];
  public allGenreString! : string;

  constructor(private mangaProductService : MangaProductService, private route : ActivatedRoute, private router: Router) {
   }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      this.mangaId = String(params.get("mangaId"));

      this.mangaProductService.getMangaData(this.mangaId).subscribe( {
        next: (res) => {
          this.mangaData = res;
          this.rating = this.mangaData.numberOfRatings != 0 ? String(Math.round(this.mangaData.sumOfRatings / this.mangaData.numberOfRatings)) : "-";  
        },
        error: (e) => this.router.navigate(['404']),
      });

      this.mangaProductService.getMangaGenres(this.mangaId).subscribe( {
        next: (res) => {
          this.allGenreString = "[";
          res.forEach((genre) => {
            this.allGenreString += genre.name + ", ";
          })
          this.allGenreString = this.allGenreString.slice(0, this.allGenreString.length - 2);
          this.allGenreString += "]";
        },
        error: (e) => this.router.navigate(['404']),
      });

      this.mangaProductService.getMangaChapters(this.mangaId).subscribe( {
        next: (res) => this.mangaChapters = res,
        error: (e) => this.router.navigate(['404']),
      });

      var item = new addToUserInfoItem();
      item.mangaId = this.mangaId;
      this.mangaProductService.addToAllReadMangas(item).subscribe((res) => {
        // window.alert("Item added to AllReadMangas!");
      });  
      this.mangaProductService.addLastReadManga(item).subscribe((res) => {
        // window.alert("Item added to lastReadmanga!");
      });
      this.mangaProductService.getUserInfo().subscribe((res) => {
        console.log(res);
      });   
    });
  }

  addToWishlist() : void {
    var item = new addToUserInfoItem();
    item.mangaId = this.mangaId;
    this.mangaProductService.addToWishlist(item).subscribe((res) => {
      window.alert("Item added to wishlist!");
      console.log(res);
    });
  }

}
