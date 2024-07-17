import {createAsyncThunk} from '@reduxjs/toolkit'
import axios from 'axios'
import {Product} from '../models/Product'
import {Bid} from '../models/Bid'
import {AddProductRequest} from './models/AddProductRequest'
import {RemoveProductRequest} from './models/RemoveProductRequest'
import {RegisterUserRequest} from './models/RegisterUserRequest'
import {RegisterUserResponse} from './models/RegisterUserResponse'
import {SignInResponse} from './models/SignInResponse'
import {SignInRequest} from './models/SignInRequest'
import AsyncStorage from '@react-native-async-storage/async-storage'
import {TransactionHistory} from '../models/TransactionHistory'
import {AddRatingRequest} from './models/AddRatingRequest'
import {UserRating} from '../models/UserRating'

const BASE_URL = 'https://barterapi.azurewebsites.net/api'

export const fetchProductsNotOwnedByUser = createAsyncThunk<Product[], string, {rejectValue: string}>(
    'products/fetchProductsNotOwnedByUser',
    async (ownerId, thunkAPI) =>
    {
        try
        {
            const response = await axios.get<Product[]>(`${BASE_URL}/Products/NotOwnedByUser/${ownerId}`)
            return response.data
        } catch (error)
        {
            return thunkAPI.rejectWithValue('Failed to fetch products')
        }
    }
)

export const fetchProductsByOwner = createAsyncThunk<Product[], string, {rejectValue: string}>(
    'products/fetchProductsByOwner',
    async (ownerId, thunkAPI) =>
    {
        try
        {
            const response = await axios.get<Product[]>(`${BASE_URL}/Products/ByOwner/${ownerId}`)
            return response.data
        } catch (error)
        {
            return thunkAPI.rejectWithValue('Failed to fetch products by owner')
        }
    }
)

export const addBid = createAsyncThunk<number, AddBidRequest, {rejectValue: string}>(
    'bids/addBid',
    async (request, thunkAPI) =>
    {
        try
        {
            const response = await axios.post<number>(`${BASE_URL}/bids`, request)
            return response.data
        } catch (error)
        {
            return thunkAPI.rejectWithValue('Failed to add bid')
        }
    }
)

export const removeBid = createAsyncThunk<number, number, {rejectValue: string}>(
    'bids/removeBid',
    async (bidId, thunkAPI) =>
    {
        try
        {
            await axios.delete(`${BASE_URL}/bids/${bidId}`)
            return bidId
        } catch (error)
        {
            return thunkAPI.rejectWithValue('Failed to remove bid')
        }
    }
)

export const fetchAllBids = createAsyncThunk<Bid[], void, {rejectValue: string}>(
    'bids/fetchAllBids',
    async (_, thunkAPI) =>
    {
        try
        {
            const response = await axios.get<Bid[]>(`${BASE_URL}/bids`)
            return response.data
        } catch (error)
        {
            return thunkAPI.rejectWithValue('Failed to fetch bids')
        }
    }
)

export const addProduct = createAsyncThunk<string, AddProductRequest, {rejectValue: string}>(
    'products/addProduct',
    async (request, thunkAPI) =>
    {
        try
        {
            const response = await axios.post<string>(`${BASE_URL}/Products`, request)
            return response.data
        } catch (error)
        {
            return thunkAPI.rejectWithValue('Failed to add product')
        }
    }
)

export const removeProduct = createAsyncThunk<void, RemoveProductRequest, {rejectValue: string}>(
    'products/removeProduct',
    async (request, thunkAPI) =>
    {
        try
        {
            await axios.delete(`${BASE_URL}/Products/${request.id}`)
        } catch (error)
        {
            return thunkAPI.rejectWithValue('Failed to remove product')
        }
    }
)

export const registerUser = createAsyncThunk<RegisterUserResponse, RegisterUserRequest, {rejectValue: string}>(
    'users/registerUser',
    async (user, thunkAPI) =>
    {
        try
        {
            const response = await axios.post<RegisterUserResponse>(`${BASE_URL}/users/register`, user)
            return response.data
        } catch (error)
        {
            return thunkAPI.rejectWithValue('Failed to register user')
        }
    }
)

export const signInUser = createAsyncThunk<SignInResponse, SignInRequest, {rejectValue: string}>(
    'users/signInUser',
    async (credentials, thunkAPI) =>
    {
        try
        {
            const response = await axios.post<SignInResponse>(`${BASE_URL}/users/signin`, credentials)

            if (response.data.errorMessage)
            {
                return thunkAPI.rejectWithValue(response.data.errorMessage)
            }

            if (!response.data.userId)
            {
                return thunkAPI.rejectWithValue('User ID not found in response')
            }

            await AsyncStorage.setItem('userId', response.data.userId)
            return response.data
        } catch (error)
        {
            return thunkAPI.rejectWithValue('Failed to sign in user')
        }
    }
)

export const addFavorite = createAsyncThunk<void, AddFavoriteRequest, {rejectValue: string}>(
    'favorites/addFavorite',
    async (request, thunkAPI) =>
    {
        try
        {
            await axios.post(`${BASE_URL}/Favorites`, request)
        } catch (error)
        {
            return thunkAPI.rejectWithValue('Failed to add favorite')
        }
    }
)

export const getUserFavorites = createAsyncThunk<Favorite[], string, {rejectValue: string}>(
    'users/getUserFavorites',
    async (userId, thunkAPI) =>
    {
        try
        {
            const response = await axios.get<Favorite[]>(`${BASE_URL}/Favorites/${userId}`)
            return response.data
        } catch (error)
        {
            return thunkAPI.rejectWithValue('Failed to fetch user favorites')
        }
    }
)

export const removeFavorite = createAsyncThunk<RemoveFavoriteRequest, RemoveFavoriteRequest, {rejectValue: string}>(
    'favorites/removeFavorite',
    async (request, thunkAPI) =>
    {
        try
        {
            await axios.delete(`${BASE_URL}/Favorites`, {data: request})
            return request
        } catch (error)
        {
            return thunkAPI.rejectWithValue('Failed to remove favorite')
        }
    }
)

export const approveBid = createAsyncThunk<number, number, {rejectValue: string}>(
    'bids/approveBid',
    async (bidId, thunkAPI) =>
    {
        try
        {
            await axios.post(`${BASE_URL}/Bids/approve/${bidId}`)
            return bidId
        } catch (error)
        {
            return thunkAPI.rejectWithValue('Failed to approve bid')
        }
    }
)

export const getAllTransactions = createAsyncThunk<TransactionHistory[], void, {rejectValue: string}>(
    'transactions/getAllTransactions',
    async (_, thunkAPI) =>
    {
        try
        {
            const response = await axios.get<TransactionHistory[]>(`${BASE_URL}/Transactions/all`)
            return response.data
        } catch (error)
        {
            return thunkAPI.rejectWithValue('Failed to fetch all transactions')
        }
    }
)

export const getUserTransactions = createAsyncThunk<TransactionHistory[], string, {rejectValue: string}>(
    'transactions/getUserTransactions',
    async (userId, thunkAPI) =>
    {
        try
        {
            const response = await axios.get<TransactionHistory[]>(`${BASE_URL}/Transactions/${userId}`)
            return response.data
        } catch (error)
        {
            return thunkAPI.rejectWithValue('Failed to fetch user transactions')
        }
    }
)

export const addRating = async (ratingRequest: AddRatingRequest): Promise<void> =>
{
    try
    {
        await axios.post(`${BASE_URL}/ratings`, ratingRequest)
    } catch (error)
    {
        throw new Error('Failed to add rating')
    }
}

export const getUserRating = async (userId: string): Promise<UserRating> =>
{
    try
    {
        const response = await axios.get<UserRating>(`${BASE_URL}/ratings/user/${userId}`)
        return response.data
    } catch (error)
    {
        throw new Error('Failed to fetch user rating')
    }
}