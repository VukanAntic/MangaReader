import { Component, HostListener, Input, OnInit } from "@angular/core";
import { Manga } from "../../domain/models/manga.model";

@Component({
  selector: "app-manga-list",
  templateUrl: "./manga-list.component.html",
  styleUrls: ["./manga-list.component.css"],
})
export class MangaListComponent implements OnInit {
  @Input()
  public mangaList?: Manga[];

  public currentPage: number = 0;
  public displayMangaList?: Manga[];
  public innerWidth: number;
  public paginationLimit: number;

  constructor() {
    this.innerWidth = window.innerWidth;
    this.paginationLimit = Math.floor((this.innerWidth - 870) / 150.0);
  }

  ngOnInit(): void {
    this.currentPage = 0;
    this.innerWidth = window.innerWidth;
    this.displayMangaList = this.mangaList?.slice(this.currentPage * this.paginationLimit, (this.currentPage + 1) * this.paginationLimit);
    //console.log('??')
  }

  @HostListener("window:resize", ["$event"])
  onResize() {
    this.innerWidth = window.innerWidth;
    console.log(this.innerWidth);
    this.paginationLimit = Math.floor((this.innerWidth - 870) / 150.0);
    console.log(this.mangaList);
    this.ngOnChanges();
  }

  ngOnChanges(): void {
    this.displayMangaList = this.mangaList?.slice(
      this.currentPage * this.paginationLimit,
      Math.min((this.currentPage + 1) * this.paginationLimit, this.mangaList.length)
    );
    //console.log(this.mangaList)
  }

  nextPage() {
    this.currentPage += 1;
    if (this.currentPage * this.paginationLimit >= this.mangaList!.length) this.currentPage = 0;
    this.ngOnChanges();
  }

  previousPage() {
    this.currentPage -= 1;
    if (this.currentPage < 0) this.currentPage = this.mangaList!.length - this.paginationLimit - 1;
    this.ngOnChanges();
  }
}
