using Agify.DAL.Abstract;
using Agify.DAL.Concrete;

namespace Agify.UnitTests.Application.UserOperations.Queries
{
    public class UserQueriesTest
    {
        private UserRepository _userRepository;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
             _userRepository = new UserRepository();
        }

        [Test]
        public async Task GetArrayAsync_WhenCalled_ReturnsArrayOfUsers()
        {
            //arrange
            var names = new string[] { "messi", "ronaldo" };
            var expectedAges = new int[] { 44, 50 };
            //act
            var result = await _userRepository.GetArrayAsync(names);

            //assert
            Assert.IsNotNull(result);
            Assert.That(result.Length, Is.EqualTo(names.Length));
            for (int i = 0; i < result.Length; i++)
            {
                Assert.That(result[i].Name, Is.EqualTo(names[i]));
                Assert.That(int.Parse(result[i].Age), Is.EqualTo(expectedAges[i]));
            }
        }

        [Test]
        public async Task GetArrayAsync_ValidNames_ReturnsArrayOfCorrectLength()
        {
            //arrange
            string[] names = { "mustafa", "mehmet", "ahmet" };

            //act
            var result = await _userRepository.GetArrayAsync(names);

            //assert
            Assert.IsNotNull(result);
            Assert.That(result.Length, Is.EqualTo(names.Length));
        }

        [Test]
        public async Task GetAsync_WithValidName_ReturnsUser()
        {
            //arrange
            var name = "baran";
            var expectedAge = 45;            

            //act
            var result = await _userRepository.GetAsync(name);

            //assert
            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.EqualTo(name));
            Assert.That(int.Parse(result.Age), Is.EqualTo(expectedAge));
        }
    }
}
