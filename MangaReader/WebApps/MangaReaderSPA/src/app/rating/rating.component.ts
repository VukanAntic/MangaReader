import { Component, Input, OnInit } from '@angular/core';
import { MangaProductService } from '../manga-product/domain/infrastructure/manga-product.service';
import { RatingItem } from '../manga-product/domain/models/ratingItem.model';

@Component({
  selector: 'app-rating',
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.css']
})
export class RatingComponent implements OnInit {

  selected = 0;
	hovered = 0;
	readonly = false;
  @Input()
  public mangaId!: string;

  constructor(private mangaService : MangaProductService) { }

  ngOnInit(): void {
  }

  onSelect() {
    this.readonly = true;
    var item = new RatingItem();
    item.id = this.mangaId;
    item.rating = this.selected;
    console.log(item);    
    this.mangaService.addRating(item);

  }

}
