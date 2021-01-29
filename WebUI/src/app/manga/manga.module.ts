import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { MangaService } from './manga.service';
import { MangaComponent } from './components/manga/manga.component';
import { HttpClientModule } from '@angular/common/http';
import { ChapterInfoComponent } from './components/chapter-info/chapter-info.component';
import { CommentComponent } from './components/comment/comment.component';
import { RandomMangaComponent } from './components/random-manga/random-manga.component';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from '../app-routing.module';
import { ChapterComponent } from './components/chapter/chapter.component';
import { BrowserModule } from '@angular/platform-browser';

@NgModule({
  declarations: [MangaComponent, ChapterInfoComponent, CommentComponent, RandomMangaComponent, ChapterComponent],
  imports: [
    CommonModule,
    SharedModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    BrowserModule
  ],
  providers:[
    MangaService
  ],
  exports:[
    MangaComponent, RandomMangaComponent
  ]
})
export class MangaModule { }
