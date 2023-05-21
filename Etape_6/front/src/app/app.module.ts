import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './Components/home/home.component';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { JwtInterceptor } from './jwt.interceptor';
import { PageNotFoundComponent } from './Components/Others/page-not-found/page-not-found.component';
import { UnauthorizeComponent } from './Components/Others/unauthorize/unauthorize.component';
import { PostsModule } from './Components/posts/posts.module';
import { AuthenticationModule } from './Components/authentication/authentication.module';
import { UsersModule } from './Components/users/users.module';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    PageNotFoundComponent,
    UnauthorizeComponent,
  ],
  imports: [
    BrowserModule,
    PostsModule,
    AuthenticationModule,
    UsersModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
  ],
  providers: [
    HttpClient,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
