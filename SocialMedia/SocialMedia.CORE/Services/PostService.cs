namespace SocialMedia.CORE.Services
{
    using SocialMedia.CORE.Entities;
    using SocialMedia.CORE.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PostService: IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public PostService(IPostRepository postRepository, IUserRepository userRepository )
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        // CREATE
        // ------
        public async Task InsertPost(Post post)
        {
            var user = await _userRepository.GetUsers(post.UserId);

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

            await _postRepository.InsertPost(post);
        }

        // READ
        // ----
        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _postRepository.GetPosts();
        }

        public async Task<Post> GetPosts(int id)
        {
            return await _postRepository.GetPosts(id);
        }

        // UPDATE
        // ------
        public async Task<bool> UpdatePost(Post post)
        {
            return await _postRepository.UpdatePost(post);
        }

        // DELETE
        // ------
        public async Task<bool> DeletePost(int id)
        {
            return await _postRepository.DeletePost(id);
        }

    }
}
