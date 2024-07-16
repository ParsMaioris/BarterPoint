import {createSelector} from '@reduxjs/toolkit'
import {RootState} from './Store'

export const selectAllProducts = createSelector(
  (state: RootState) => state.products.userProducts,
  (state: RootState) => state.products.products,
  (userProducts, products) => [...userProducts, ...products]
)

export const selectFavorites = (state: RootState) => state.users.favorites

export const selectFavoriteProducts = createSelector(
  [selectFavorites, selectAllProducts],
  (favorites, allProducts) =>
  {
    return favorites.map(favorite => ({
      ...favorite,
      ...allProducts.find(product => product.id === favorite.productId)
    })).filter(favorite => favorite.id !== undefined)
  }
)