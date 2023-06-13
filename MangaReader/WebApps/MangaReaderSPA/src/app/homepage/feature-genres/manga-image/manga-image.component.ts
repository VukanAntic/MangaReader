import { Component, OnInit, Input } from '@angular/core'
import { Manga } from '../../domain/models/manga.model'

@Component({
  selector: 'app-manga-image',
  templateUrl: './manga-image.component.html',
  styleUrls: ['./manga-image.component.css'],
})
export class MangaImageComponent implements OnInit {
  @Input()
  manga!: Manga
  constructor() {}

  ngOnInit(): void {}
}
