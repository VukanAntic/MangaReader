import { Component, OnChanges, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Manga } from '../manga-product/domain/models/manga.model';
import { SearchService } from './domain/infrastructure/search.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit  {

  public searchPrefix! : string;
  private paramMapSub : Subscription;
  public searchResultMangas! : Manga[];

  constructor(private route : ActivatedRoute, private searchService : SearchService) { 
    this.paramMapSub = this.route.paramMap.subscribe(params => {
      this.searchPrefix = String(params.get("searchPrefix"));
      console.log(this.searchPrefix);
      this.ngOnInit();
    })

  }

  ngOnInit(): void {
    this.searchService.getSearchMangas(this.searchPrefix).subscribe(
      (res) => {
        this.searchResultMangas = res;
        console.log(this.searchResultMangas);
      }, 
    );
  }

  ngOnDestroy() {
    if (this.paramMapSub != null) {
      this.paramMapSub.unsubscribe();
    }
  }

}
