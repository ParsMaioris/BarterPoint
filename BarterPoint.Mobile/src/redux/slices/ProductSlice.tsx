import {createSlice, PayloadAction} from '@reduxjs/toolkit'
import {fetchProductsByOwner, fetchProductsNotOwnedByUser} from '../../api/ApiService'
import {addProduct, removeProduct} from '../../api/ApiService'
import {Product} from '../../models/Product'

interface ProductState
{
  userProducts: Product[]
  products: Product[]
  status: 'idle' | 'loading' | 'succeeded' | 'failed'
  error: string | null
}

const initialState: ProductState = {
  userProducts: [],
  products: [],
  status: 'idle',
  error: null,
}

const productSlice = createSlice({
  name: 'products',
  initialState,
  reducers: {
    addUserItem: (state, action: PayloadAction<Product>) =>
    {
      state.userProducts.push(action.payload)
    },
    removeUserItem: (state, action: PayloadAction<string>) =>
    {
      state.userProducts = state.userProducts.filter((item) => item.id !== action.payload)
    },
  },
  extraReducers: (builder) =>
  {
    builder
      .addCase(fetchProductsNotOwnedByUser.pending, (state) =>
      {
        state.status = 'loading'
      })
      .addCase(fetchProductsNotOwnedByUser.fulfilled, (state, action: PayloadAction<Product[]>) =>
      {
        state.status = 'succeeded'
        state.products = action.payload
      })
      .addCase(fetchProductsNotOwnedByUser.rejected, (state, action) =>
      {
        state.status = 'failed'
        state.error = action.error.message || 'Failed to fetch products'
      })
      .addCase(fetchProductsByOwner.pending, (state) =>
      {
        state.status = 'loading'
      })
      .addCase(fetchProductsByOwner.fulfilled, (state, action: PayloadAction<Product[]>) =>
      {
        state.status = 'succeeded'
        state.userProducts = action.payload
      })
      .addCase(fetchProductsByOwner.rejected, (state, action) =>
      {
        state.status = 'failed'
        state.error = action.error.message || 'Failed to fetch products by owner'
      })
      .addCase(addProduct.pending, (state) =>
      {
        state.status = 'loading'
      })
      .addCase(addProduct.fulfilled, (state) =>
      {
        state.status = 'succeeded'
      })
      .addCase(addProduct.rejected, (state, action) =>
      {
        state.status = 'failed'
        state.error = action.error.message || 'Failed to add product'
      })
      .addCase(removeProduct.pending, (state) =>
      {
        state.status = 'loading'
      })
      .addCase(removeProduct.fulfilled, (state, action) =>
      {
        state.status = 'succeeded'
        state.userProducts = state.userProducts.filter((product) => product.id !== action.meta.arg.id)
      })
      .addCase(removeProduct.rejected, (state, action) =>
      {
        state.status = 'failed'
        state.error = action.error.message || 'Failed to remove product'
      })
  },
})

export const {addUserItem, removeUserItem} = productSlice.actions

export default productSlice.reducer