namespace SocialMedia.CORE.Services
{
    using SocialMedia.CORE.Entities;
    using SocialMedia.CORE.Interfaces;
    using System;
    using System.Collections.Generic;
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
                throw new Exception("El usuario no existe");
            }

            // Contiene la palabra sexo
            if (post.Description.Contains("Sexo")) 
            {
                throw new Exception("Contenido no permitido");
            }

            await _unitOfWork.PostRepository.Add(post);
        }

        // READ
        // ----
        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _unitOfWork.PostRepository.GetAll();
        }

        public async Task<Post> GetPosts(int id)
        {
            return await _unitOfWork.PostRepository.GetById(id);
        }

        // UPDATE
        // ------
        public async Task<bool> UpdatePost(Post post)
        {
            await _unitOfWork.PostRepository.Update(post);
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
