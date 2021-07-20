namespace SocialMedia.CORE.Services
{
    using SocialMedia.CORE.Entities;
    using SocialMedia.CORE.Exceptions;
    using SocialMedia.CORE.Interfaces;
    using SocialMedia.CORE.QueryFilters;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PostService: IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // CREATE
        // ------
        public async Task InsertPost(Post post)
        {
            var user = await _unitOfWork.UserRepository.GetById(post.UserId);

            // Exixtencia del usuario
            if (user == null) 
            {
                throw new BusinessException("El usuario no existe");
            }

            // Puede realizar publicacion
            var userPost = await _unitOfWork.PostRepository.GetPostsByUser(post.UserId);
            if (userPost.Count() < 10) 
            {
                var lastPost = userPost.OrderByDescending(x=>x.Date).FirstOrDefault();
                if ((DateTime.Now - lastPost.Date).TotalDays < 7) 
                {
                    throw new BusinessException("Usted no puede hacer la publicacion");
                }
            }

            // Contiene la palabra sexo
            if (post.Description.Contains("Sexo")) 
            {
                throw new BusinessException("Contenido no permitido");
            }

            await _unitOfWork.PostRepository.Add(post);
            await _unitOfWork.SaveChanesAsync();
        }

        // READ
        // ----
        public IEnumerable<Post> GetPosts(PostQueryFilter filters)
        {
            var post = _unitOfWork.PostRepository.GetAll();
            
            // Validacion de filters.
            if (filters.UserId != null) 
                post = post.Where(x => x.UserId == filters.UserId);

            if (filters.Date != null)
                post = post.Where(x => x.Date.ToShortDateString() == filters.Date?.ToShortDateString());

            if (filters.Description != null)
                post = post.Where(x => x.Description.ToLower().Contains(filters.Description.ToLower()));

            return post;
        }

        public async Task<Post> GetPosts(int id)
        {
            return await _unitOfWork.PostRepository.GetById(id);
        }

        // UPDATE
        // ------
        public async Task<bool> UpdatePost(Post post)
        {
            _unitOfWork.PostRepository.Update(post);
            await _unitOfWork.SaveChanesAsync();
            return true;
        }

        // DELETE
        // ------
        public async Task<bool> DeletePost(int id)
        {
            await _unitOfWork.PostRepository.Delete(id);
            return true;
        }

    }
}
