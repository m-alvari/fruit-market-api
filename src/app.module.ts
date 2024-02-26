import { Module } from "@nestjs/common";
import { AppController } from "./app.controller";
import { AppService } from "./app.service";
import { ProductsController } from "./products/products.controller";
import { ProductService } from "./products/services/product/product.service";

@Module({
  imports: [],
  controllers: [AppController, ProductsController],
  providers: [AppService, ProductService],
})
export class AppModule {}
