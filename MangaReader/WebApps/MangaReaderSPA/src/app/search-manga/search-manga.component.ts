import { Component, Input, OnInit } from '@angular/core';
import { Manga } from '../manga-product/domain/models/manga.model';

@Component({
  selector: 'app-search-manga',
  templateUrl: './search-manga.component.html',
  styleUrls: ['./search-manga.component.css']
})
export class SearchMangaComponent implements OnInit {

  @Input()
  public manga!: Manga;

  constructor() { }

  ngOnInit(): void {
  }

}
