import { Component, Input, OnInit } from '@angular/core'
import { Manga } from '../../domain/models/manga.model'

@Component({
  selector: 'app-manga-list',
  templateUrl: './manga-list.component.html',
  styleUrls: ['./manga-list.component.css'],
})
export class MangaListComponent implements OnInit {
  @Input()
  public mangaList?: Manga[]
  constructor() {}

  ngOnInit(): void {}
}
